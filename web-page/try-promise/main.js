function demo1() {
    const p = new Promise((res, _) => {
        console.log("in promise 1");
        console.log("in promise 2");
        console.log("in promise 3");
        setTimeout(_ => {
            console.log("after timeout 1");
            console.log("after timeout 2");
            console.log("after timeout 3");
            res();
        }, 3000);
    });
    
    console.log('after new promise');
    
    p
        .then(_ => console.log('then 1'))
        .then(_ => console.log('then 2'))
        .then(_ => console.log('then 3'));
    
    console.log('after then');
}

async function demo2() {
    const p = new Promise((res, _) => {
        console.log("in promise 1");
        console.log("in promise 2");
        console.log("in promise 3");
        setTimeout(_ => {
            console.log("after timeout 1");
            console.log("after timeout 2");
            console.log("after timeout 3");
            res();
        }, 3000);
    });

    console.log('after new promise');

    await p;
    console.log('then 1');
    console.log('then 2');
    console.log('then 3');

    console.log('after then');
}

// demo1();
// Output:
//      in promise 1
//      in promise 2
//      in promise 3
//      after new promise
//      after then
//      after timeout 1
//      after timeout 2
//      after timeout 3
//      then 1
//      then 2
//      then 3

demo2();
// Output:
//      in promise 1
//      in promise 2
//      in promise 3
//      after new promise
//      after timeout 1
//      after timeout 2
//      after timeout 3
//      then 1
//      then 2
//      then 3
//      after then

// Both demo not block UI while in `setTimeout()` 3 seconds.
