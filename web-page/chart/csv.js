class Csv {
    constructor(rawText) {
        this.Seperator = ',';
        this.LineEndingRegex = /\r?\n/;

        this.header = [];
        this.records = [];
        this._readData(rawText);
    }

    _readData(rawText) {
        let lines = rawText.split(this.LineEndingRegex);
        this.header = lines[0].split(this.Seperator);
        lines.shift();
        lines.forEach(line => {
            this.records.push(line.split(this.Seperator));
        });
    }

    getColumn(colHeader) {
        let index = this.header.indexOf(colHeader);
        if (!index) { return []; }
        return this.records.map(item => item[index]);
    }
}