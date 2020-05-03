import { interval } from 'rxjs';
import { take, share } from 'rxjs/operators';

export function demoShare(): void {
    let observable = interval(500).pipe(take(5), share());
    
    observable.subscribe(x => console.log('first', x));
    setTimeout(() => observable.subscribe(x => console.log('second', x)), 2000);
}
