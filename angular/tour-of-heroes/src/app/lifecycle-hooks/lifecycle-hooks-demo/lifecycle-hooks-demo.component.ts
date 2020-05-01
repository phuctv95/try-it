import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-lifecycle-hooks-demo',
  templateUrl: './lifecycle-hooks-demo.component.html',
  styleUrls: ['./lifecycle-hooks-demo.component.less']
})
export class LifecycleHooksDemoComponent implements OnInit {

  card1: string;
  card2: string[] = [];
  highlightColor: string;
  s1: string = 'abc';

  constructor() { }

  ngOnInit(): void {
    console.log(`${LifecycleHooksDemoComponent.name} OnInit`);
    this.changeCardContent();
    this.changeHighlightColor();
  }

  changeCardContent(): void {
    let now = new Date().toLocaleTimeString();
    this.card1 = now;
    this.card2 = [...this.card2, now];
  }

  changeHighlightColor(): void {
    this.highlightColor = this.randomColor();
  }

  randomColor(): string {
    const letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
      color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
  }
  
  doNothing(): void { }

  setTimeoutAndDoNothing(): void {
    setTimeout(() => {}, 1000);
  }
  
}
