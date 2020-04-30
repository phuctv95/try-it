import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.less']
})
export class CardComponent implements OnInit, OnChanges {

  @Input() content: string;

  @Input() contents: string[];

  constructor() { }

  ngOnChanges(changes: SimpleChanges) {
    console.log('CardComponent OnChanges:', changes);
  }
  
  ngOnInit(): void {
  }

}
