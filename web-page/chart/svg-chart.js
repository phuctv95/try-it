class SvgChart {
    constructor(axisId, axisHeight) {
        this.Height = axisHeight;
        this.SpaceBetweenValue = 5;
        this.axisId = axisId;
        this.maxValue = 0;
        this.numberOfValues = 0;
        this.paths = [];
    }

    addPath(path) {
        this.paths.push(path);
        this.updateData(path.values);
        this.updateUiAxis();
        this.updateUiPathsValue();
    }

    updateData(values) {
        let max = Math.max(...values.filter(x => x || x === 0));
        if (max > this.maxValue) { this.maxValue = max; }

        if (values.length > this.numberOfValues) { this.numberOfValues = values.length; }
    }

    updateUiAxis() {
        let points = `0,0 0,${this.Height} ${(this.numberOfValues + 1) * this.SpaceBetweenValue},${this.Height}`;
        let polyline = document.querySelector(`#${this.axisId}`);
        polyline.setAttribute('points', points);
    }

    updateUiPathsValue() {
        this.paths.forEach(path => {
            let polyline = document.querySelector(`#${path.id}`);
            let points = this._valuesToPoints(path.values);
            polyline.setAttribute('points', points);
        });
    }

    _valuesToPoints(values) {
        let points = '';
        let x = 0;
        values.forEach(value => {
            if (!value && value !== 0) { return; }
            points += `${x},${this.Height - value * this.Height / this.maxValue} `;
            x += this.SpaceBetweenValue;
        });
        return points;
    }
}