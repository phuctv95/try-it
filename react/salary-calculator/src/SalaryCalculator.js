import { Component } from 'react';
import Header from './Header';
import DataInput from './DataInput';
import CalculatedResult from './CalculatedResult';

class SalaryCalculator extends Component {
    constructor(props) {
        super(props);

        this.handleGrossChange = this.handleGrossChange.bind(this);

        this.state = {
            gross: 0
        };
    }

    handleGrossChange(gross) {
        this.setState({
            gross: gross
        });
    }

    render() {
        return (
            <div>
                <Header/>
                <DataInput gross={this.state.gross} onGrossChange={this.handleGrossChange}/>
                <CalculatedResult gross={this.state.gross}/>
            </div>
        );
    }
}

export default SalaryCalculator;
