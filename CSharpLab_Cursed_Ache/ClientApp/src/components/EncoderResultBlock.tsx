import * as React from "react";
import { RouteComponentProps } from 'react-router';
import { connect } from "react-redux";
import * as EncoderStore from "../store/Encoder";
import { ApplicationState } from "../store";
import { Input, Button, Spinner, UncontrolledAlert } from "reactstrap";

type EncoderProps =
    EncoderStore.EncoderResultState &
    typeof EncoderStore.actionCreators &
    RouteComponentProps<{}>;

class EncoderResultBlock extends React.PureComponent<EncoderProps>{

    constructor(props: Readonly<EncoderProps>) {
        super(props);
        this.onDownloadClick = this.onDownloadClick.bind(this);
    }

    onDownloadClick(type: string) {
        fetch(`api/downloadfile`, {
            method: "POST",
            body: JSON.stringify({ text: this.props.text, type: type }),
            headers: { 'Content-Type': 'application/json' }
        }).then(resp => resp.blob()).then((blob) => {
            const url = window.URL.createObjectURL(new Blob([blob]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', `result.${type}`);
            document.body.appendChild(link);
            document.body.removeChild(link);
            link.click();
        });
    }

    public render() {

        return (
            <div className="borderRounded filled padding margin box-shadow">
                {this.props.errorMessage &&
                    <UncontrolledAlert color="danger">
                        {this.props.errorMessage}
                    </UncontrolledAlert>    }
                <Input type="textarea" className="text-area" readOnly={true} value={this.props.text} />
                <div className="padding-top">
                    <Button type="button" onClick={() => this.onDownloadClick("txt")} >Скачать .txt</Button>
                    <Button className="margin-left" type="button" onClick={() => this.onDownloadClick("docx")} >Скачать .docx</Button>
                </div>
            </div>
        );
    }
}


export default connect(
    (state: ApplicationState) => state.encoderResult,
    EncoderStore.actionCreators
)(EncoderResultBlock as any);