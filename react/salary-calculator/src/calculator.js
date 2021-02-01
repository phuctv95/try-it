const OneMillion = 1000000;

function calculateTax(taxableIncome) {
    if (taxableIncome <= 5 * OneMillion) {
        return 0.05 * taxableIncome;
    }
    if (taxableIncome <= 10 * OneMillion) {
        return 0.1 * taxableIncome - 0.25 * OneMillion;
    }
    if (taxableIncome <= 18 * OneMillion) {
        return 0.15 * taxableIncome - 0.75 * OneMillion;
    }
    if (taxableIncome <= 32 * OneMillion) {
        return 0.2 * taxableIncome - 1.65 * OneMillion;
    }
    if (taxableIncome <= 52 * OneMillion) {
        return 0.25 * taxableIncome - 3.25 * OneMillion;
    }
    if (taxableIncome <= 80 * OneMillion) {
        return 0.3 * taxableIncome - 5.85 * OneMillion;
    }
    return 0.35 * taxableIncome - 9.85 * OneMillion;
}

function calculateNet(gross) {
    let result = {};
    result.socialInsurance = gross * 0.08;
    result.healthInsurance = gross * 0.015;
    result.unemploymentInsurance = gross * 0.01;
    result.incomeBeforeTax = gross - (result.socialInsurance +
        result.healthInsurance + result.unemploymentInsurance);
    result.personalReduction = 11 * OneMillion;
    result.taxableIncome = result.incomeBeforeTax - result.personalReduction;
    result.taxableIncome = result.taxableIncome < 0 ? 0 : result.taxableIncome;
    result.tax = calculateTax(result.taxableIncome);
    result.net = result.incomeBeforeTax - result.tax;

    return result;
}

export default calculateNet;
