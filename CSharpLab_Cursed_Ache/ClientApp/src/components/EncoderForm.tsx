import * as React from "react";
import { Button, Input, CustomInput, UncontrolledPopover, PopoverHeader, PopoverBody,  Form } from "reactstrap";
import { RouteComponentProps } from 'react-router';
import { connect } from "react-redux";
import * as EncoderStore from "../store/Encoder";
import { ApplicationState } from "../store";
import $ from "jquery";

type EncoderProps =
    EncoderStore.EncoderFormState &
    typeof EncoderStore.actionCreators &
    RouteComponentProps<{}>;

interface State {
    text: string;
    key: string;
    action: boolean;
    file: File | null
}

class EncoderForm extends React.PureComponent<EncoderProps, State>{ 


    constructor(props: Readonly<EncoderProps>) {
        super(props);
        this.onSubmitForm = this.onSubmitForm.bind(this);
        this.state = {
            text: this.props.text,
            key: this.props.key,
            action: true,
            file: this.props.file
        }
    }


    onSubmitForm() {
        let text: string = this.state.text;
        let key: string = this.state.key;
        let action: string = this.state.action ? "encrypt" : "decrypt";
        let file: File | null = this.state.file;
        this.props.formDataChangeAction({ text, key, action, file })
        this.props.submitEncoderForm();
        this.setState({ file: null });
        $('#fileInput').val('');
    }
    

    public render() {        
        return (
            <div>
                <Form>
                    <div className="borderRounded filled padding margin box-shadow">
                        <h5>Текст</h5>
                            <Input type="textarea" className="text-area" required={true} onChange={(e) => { this.setState({ text: e.target.value }) }} defaultValue={this.props.text} />
                        
                        <h5>Ключ</h5>
                            <Input type="text" required={true} onChange={(e) => { this.setState({ key: e.target.value }) }} defaultValue={this.props.key} />
                        
                        <br/>
                        <p className="cursive"><i>Или </i>
                            <Button id="UncontrolledPopover" type="button" color="info" className="margin-bottom">
                                <b>i</b>
                            </Button>
                            <UncontrolledPopover placement="top" target="UncontrolledPopover">
                                <PopoverHeader>Нужный формат</PopoverHeader>
                                <PopoverBody>
                                    Выберите .txt или .docx файл с содержимым вида:<br />
                                    text:ВАШ_ТЕКСТ<br />
                                    key:ВАШ_КЛЮЧ
                                </PopoverBody>
                            </UncontrolledPopover>
                        </p>
                        <Input type="file" id="fileInput" onChange={(e) => this.setState({ file: e.target.files![0] })} label="Выберите файл" />
                    </div>

                    
                    <div className="borderRounded filled padding margin box-shadow">
                        <h5>Действие</h5>
                        <div className="margin">
                            <Input type="radio" name="action" value="encrypt" checked={this.state.action} onClick={(e) => { this.setState({ action: true }) }} /> Зашифровать<br />
                            <Input type="radio" name="action" value="decrypt" checked={!this.state.action} onClick={(e) => { this.setState({ action: false }) }} /> Расшифровать<br />
                        </div>
                    
                        <Button type="button" className="btn btn-secondary" onClick={this.onSubmitForm} >Отправить</Button>
                    </div>
                </Form>

                
                
            </div>
        );
    }
}


export default connect(
    (state: ApplicationState) => state.encoderForm,
    EncoderStore.actionCreators
)(EncoderForm as any);
