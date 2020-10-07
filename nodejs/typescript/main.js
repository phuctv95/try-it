"use strict";
exports.__esModule = true;
var decimal_js_1 = require("decimal.js");
console.log(new decimal_js_1.Decimal("1").toFixed(10));
console.log(new decimal_js_1.Decimal("1.0").toFixed(10));
console.log(new decimal_js_1.Decimal("1.1").toFixed(10));
