const OneMillion = 1000000;

function calculateTax(taxableIncome) {
    if (taxableIncome <= 5 * OneMillion) {
        return 0.05 * taxableIncome;
    }
    if (taxableIncome <= 10 * OneMillion) {
        return 0.25 * OneMillion + 0.1 * taxableIncome;
    }
    if (taxableIncome <= 18 * OneMillion) {
        return 0.75 * OneMillion + 0.15 * taxableIncome;
    }
    if (taxableIncome <= 32 * OneMillion) {
        return 1.95 * OneMillion + 0.2 * taxableIncome;
    }
    if (taxableIncome <= 52 * OneMillion) {
        return 4.75 * OneMillion + 0.25 * taxableIncome;
    }
    if (taxableIncome <= 80 * OneMillion) {
        return 9.75 * OneMillion + 0.3 * taxableIncome;
    }
    return 18.15 * OneMillion + 0.35 * taxableIncome;
}

function calculateNet(gross) {
    let result = {
        socialInsurance: 0,
        healthInsurance: 0,
        unemploymentInsurance: 0,
        incomeBeforeTax: 0,
        personalReduction: 0,
        taxableIncome: 0,
        tax: 0,
        net: 0,
    };
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
