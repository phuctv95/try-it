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
                <label>Gross:
                    <input value={this.props.gross} onChange={this.handleGrossChange}/>
                </label>
            </div>
        );
    }
}

export default DataInput;
