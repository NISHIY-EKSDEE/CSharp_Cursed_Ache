import * as React from "react";
import { connect } from "react-redux";
import EncoderForm from "./EncoderForm";
import EncoderResultBlock from "./EncoderResultBlock";

const Home = () => (
    <div className="margin padding">
        <EncoderForm />
        <EncoderResultBlock />
    </div>
);


export default connect()(Home);
