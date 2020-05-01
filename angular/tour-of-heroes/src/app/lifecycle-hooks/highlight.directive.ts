import { Directive, ElementRef, Input, OnChanges, SimpleChanges, DoCheck } from '@angular/core';

@Directive({
  selector: '[appHighlight]'
})
export class HighlightDirective implements OnChanges, DoCheck {

  @Input() highlightColor: string;

  constructor(private el: ElementRef) { }

  ngOnChanges(changes: SimpleChanges) {
    console.log(`${HighlightDirective.name} OnChanges`);
    this.el.nativeElement.style.backgroundColor = this.highlightColor;
  }

  ngDoCheck() {
    // In default strategy, this is called everytime has browser events,
    // timers, XHRs and promises.
    console.log(`${HighlightDirective.name} DoCheck`);
  }
  
}
