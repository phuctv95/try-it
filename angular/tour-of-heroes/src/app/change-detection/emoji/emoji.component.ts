import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-emoji',
  templateUrl: './emoji.component.html',
  styleUrls: ['./emoji.component.less']
})
export class EmojiComponent implements OnInit, OnChanges {

  @Input() name: string;
  emoji: string;

  constructor() { }

  ngOnChanges(changes: SimpleChanges) {
    if (this.name === 'smile') { this.emoji = ':)'; }
  }

  ngOnInit(): void {
  }

}
