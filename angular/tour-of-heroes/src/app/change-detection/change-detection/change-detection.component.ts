import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-change-detection',
  templateUrl: './change-detection.component.html',
  styleUrls: ['./change-detection.component.less']
})
export class ChangeDetectionComponent implements OnInit {

  foods = ['Bacon', 'Lettuce', 'Tomatoes'];

  constructor() { }

  ngOnInit(): void {
  }

  addFood(food: string): void {
    this.foods = [...this.foods, food];
  }

}
