import { Component } from 'react';

class DataInput extends Component {
    constructor(props) {
        super(props);
        this.handleGrossChange = this.handleGrossChange.bind(this);
    }

    handleGrossChange(e) {
        this.props.onGrossChange(e.target.value);
    }
    
    render() {
        return (
            <div>
                <label htmlFor="gross">GROSS:</label>
                <div className="input-group mb-3">
                    <input value={this.props.gross} onChange={this.handleGrossChange}
                        type="text" className="form-control" id="gross"/>
                </div>
            </div>
        );
    }
}

export default DataInput;
