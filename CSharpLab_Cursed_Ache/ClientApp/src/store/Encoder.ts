import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';
import $ from "jquery";

export interface EncoderFormState {
    text: string;
    key: string;
    action: string;
    file: File | null;
    isFileUploaded: boolean;
}

export interface EncoderResultState {
    text: string;
    received: boolean;
    errorMessage: string | null;
}

export interface TextChangedAction { type: 'TEXT_CHANGED', payload: {text: string} }
export interface KeyChangedAction { type: 'KEY_CHANGED', payload: { key: string } }
export interface ActionChangedAction { type: 'ACTION_CHANGED', payload: { action: string } }
export interface EncoderFormDataChangedAction { type: 'FORM_DATA_CHANGED', payload: { text: string, key: string, action: string; file: File } }
export interface EncoderRequestAction { type: 'ENCODER_REQUEST' }
export interface EncoderResponseAction { type: 'ENCODER_RECEIVE', payload: { text: string, isSuccess: boolean, errorMessage: string | null } }
export interface DownloadFileAction { type: 'DOWNLOAD_FILE', payload: {format: string} }
export interface ResetFileAction { type: 'RESET_FILE'}

export type KnownAction = TextChangedAction | KeyChangedAction | ActionChangedAction | EncoderRequestAction | EncoderResponseAction | EncoderFormDataChangedAction | ResetFileAction;

export const actionCreators = {
    changeText: (s: string) => ({
        type: 'TEXT_CHANGED', payload: { text: s }
    } as TextChangedAction),

    changeKey: (s: string) => ({
        type: 'KEY_CHANGED', payload: { key: s }
    } as KeyChangedAction),

    changeAction: (s: string) => ({
        type: 'ACTION_CHANGED', payload: { action: s }
    } as ActionChangedAction),

    formDataChangeAction: ( payload: { text: string, key: string, action: string, file: File | null }) => ({
        type: 'FORM_DATA_CHANGED', payload: payload
    } as EncoderFormDataChangedAction),

    submitEncoderForm: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState.encoderForm!.isFileUploaded) {
            let formdata = new FormData();
            formdata.append("file", appState.encoderForm!.file!);
            formdata.append("action", appState.encoderForm!.action);

            let requestOptions = {
                method: 'POST',
                body: formdata,
            };

            fetch("api/encodefile", requestOptions)
                .then(response => response.json())
                .then(data => {
                    dispatch({ type: 'RESET_FILE' });
                    dispatch({ type: 'ENCODER_RECEIVE', payload: { text: data.result, isSuccess: data.isSuccess, errorMessage: data.errorMessage } });
                });

            dispatch({ type: 'ENCODER_REQUEST' });
        }
        else if (appState.encoderForm!.text.trim() !== "" && appState.encoderForm!.key.trim() !== "") {
            fetch(`api/encodetext`, {
                method: "POST",
                body: JSON.stringify({ text: appState.encoderForm!.text, key: appState.encoderForm!.key, action: appState.encoderForm!.action}),
                headers: {'Content-Type': 'application/json'}
            })
                .then(response => response.json() )
                .then(data => {
                    dispatch({
                        type: 'ENCODER_RECEIVE', payload: { text: data.result, isSuccess: data.isSuccess, errorMessage: data.errorMessage }
                    });
                    
                });

            dispatch({ type: 'ENCODER_REQUEST' });
        }
    } 
};

export const formReducer: Reducer<EncoderFormState> = (state: EncoderFormState | undefined, incomingAction: Action): EncoderFormState => {
    if (state === undefined) {
        return {
            text: "",
            key: "",
            action: "",
            file: null,
            isFileUploaded: false
        };
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {        
        case 'TEXT_CHANGED':
            return {
                text: action.payload.text,
                key: state.key,
                action: state.action,
                file: state.file,
                isFileUploaded: state.isFileUploaded
            };
        case 'KEY_CHANGED':
            return {
                text: state.text,
                key: action.payload.key,
                action: state.action,
                file: state.file,
                isFileUploaded: state.isFileUploaded
            };
        case 'ACTION_CHANGED':
            return {
                text: state.text,
                key: state.key,
                action: action.payload.action,
                file: state.file,
                isFileUploaded: state.isFileUploaded
            };
        case 'FORM_DATA_CHANGED':
            return {
                text: action.payload.text,
                key: action.payload.key,
                action: action.payload.action,
                file: action.payload.file,
                isFileUploaded: action.payload.file ? true : false
            };
        case 'RESET_FILE':
            $("#fileInput").val('');
            return {
                text: state.text,
                key: state.key,
                action: state.action,
                file: null,
                isFileUploaded: false
            };
        default:
            return state;
    }
};

export const resultReducer: Reducer<EncoderResultState> = (state: EncoderResultState | undefined, incomingAction: Action): EncoderResultState => {
    if (state === undefined) {
        return {
            text: "",
            received: false,
            errorMessage: null
        };
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'ENCODER_REQUEST':
            return {
                text: "",
                received: false,
                errorMessage: null
            };
        case 'ENCODER_RECEIVE':
            let text: string;
            let errMsg: string | null;
            if (action.payload.isSuccess) {
                text = action.payload.text;
                errMsg = null;
            } else {
                text = "";
                errMsg = action.payload.errorMessage;
            }
            return {
                text: text,
                received: true,
                errorMessage: errMsg
            };
        default:
            return state;
    }
};