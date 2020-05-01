import { Directive, ElementRef, Input, OnChanges, SimpleChanges } from '@angular/core';

@Directive({
  selector: '[appHighlight]'
})
export class HighlightDirective implements OnChanges {

  @Input() highlightColor: string;

  constructor(private el: ElementRef) { }

  ngOnChanges(changes: SimpleChanges) {
    this.el.nativeElement.style.backgroundColor = this.highlightColor;
  }
  
}
