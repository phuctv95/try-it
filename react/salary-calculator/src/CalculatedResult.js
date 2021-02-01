import { Component } from 'react';
import calculateNet from './calculator';

class CalculatedResult extends Component {
    render() {
        let calculated = calculateNet(this.props.gross);
        return (
            <table className="table table-sm table-bordered table-hover mt-3">
                <tbody>
                    <tr>
                        <td>GROSS</td>
                        <td className="text-right">{(+this.props.gross).toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>BHXH (8%)</td>
                        <td className="text-right">-{calculated.socialInsurance.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>BHYT (1.5%)</td>
                        <td className="text-right">-{calculated.healthInsurance.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>BHTN (1%)</td>
                        <td className="text-right">-{calculated.unemploymentInsurance.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>Thu nhập trước thuế</td>
                        <td className="text-right">{calculated.incomeBeforeTax.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>Giảm trừ gia cảnh bản thân</td>
                        <td className="text-right">-{calculated.personalReduction.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>Thu nhập chịu thuế</td>
                        <td className="text-right">{calculated.taxableIncome.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>Thuế thu nhập cá nhân</td>
                        <td className="text-right">{calculated.tax.toLocaleString()}</td>
                    </tr>
                    <tr>
                        <td>NET</td>
                        <td className="text-right">{calculated.net.toLocaleString()}</td>
                    </tr>
                </tbody>
            </table>
        );
    }
}

export default CalculatedResult;
