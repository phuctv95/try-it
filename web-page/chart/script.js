const data = {
    Url: 'https://covid.ourworldindata.org/data/ecdc/total_cases.csv',
    csv: null,
    VnHeader: 'Vietnam',
    SvgId: 'chart',
    VnPolygonId: 'vn-path',
    AxisId: 'axis',
    chart: null
};

window.addEventListener('load', () => {
    fetch(data.Url).then(
        response => response.text().then(onFetchCovidDataSuccess)
    );
});

function getHeightOfSvg(id) {
    return document.querySelector(`#${id}`).getBoundingClientRect().height;
}

function onFetchCovidDataSuccess(rawText) {
    data.csv = new Csv(rawText);
    data.chart = new SvgChart(data.AxisId, getHeightOfSvg(data.SvgId));
    let vieCol = data.csv.getColumn(data.VnHeader).map(x => x ? +x : x);
    data.chart.addPath({ id: data.VnPolygonId, values: vieCol });
}