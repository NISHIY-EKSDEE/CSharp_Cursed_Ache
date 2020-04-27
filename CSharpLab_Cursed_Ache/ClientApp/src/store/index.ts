import * as Encoder from './Encoder';

export interface ApplicationState {
    encoderForm: Encoder.EncoderFormState | undefined;
    encoderResult: Encoder.EncoderResultState | undefined;
}

export const reducers = {
    encoderForm: Encoder.formReducer,
    encoderResult: Encoder.resultReducer
};

export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
