import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-lifecycle-hooks-demo',
  templateUrl: './lifecycle-hooks-demo.component.html',
  styleUrls: ['./lifecycle-hooks-demo.component.less']
})
export class LifecycleHooksDemoComponent implements OnInit {

  card1: string;
  card2: string[] = [];

  constructor() { }

  ngOnInit(): void {
    console.log(`${LifecycleHooksDemoComponent.name} OnInit`);
    this.changeCardContent();
  }

  changeCardContent(): void {
    let now = new Date().toLocaleTimeString();
    this.card1 = now;
    this.card2 = [...this.card2, now];
  }

}
