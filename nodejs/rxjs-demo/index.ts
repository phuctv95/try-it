import { demoShare } from "./demo-share";
import { interval, Observable, range, concat, merge, of } from "rxjs";
import { take, map, filter, startWith, scan, distinctUntilChanged, debounceTime, tap, reduce } from "rxjs/operators";

of(1, 2, 3, 4).pipe(reduce((acc, value) => acc + value)).subscribe(x => console.log(x));
