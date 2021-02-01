import './CalculatedResult.css';
import { Component } from 'react';
import calculateNet from './calculator';

class CalculatedResult extends Component {
    render() {
        let calculated = calculateNet(this.props.gross);
        return (
            <table>
                <tbody>
                    <tr>
                        <td>GROSS</td>
                        <td>{(+this.props.gross).toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>BHXH (8%)</td>
                        <td>-{calculated.socialInsurance.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>BHYT (1.5%)</td>
                        <td>-{calculated.healthInsurance.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>BHTN (1%)</td>
                        <td>-{calculated.unemploymentInsurance.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>Thu nhập trước thuế</td>
                        <td>{calculated.incomeBeforeTax.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>Giảm trừ gia cảnh bản thân</td>
                        <td>-{calculated.personalReduction.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>Thu nhập chịu thuế</td>
                        <td>{calculated.taxableIncome.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>Thuế thu nhập cá nhân</td>
                        <td>{calculated.tax.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>NET</td>
                        <td>{calculated.net.toLocaleString()}</td>
                    </tr>
                </tbody>
            </table>
        );
    }
}

export default CalculatedResult;
