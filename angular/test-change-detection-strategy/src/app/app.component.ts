import { ChangeDetectorRef, Component, DoCheck } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements DoCheck {

  title = 'test-change-detection-strategy';
  data = {
    now: Date.now(),
  };

  constructor(private cdr: ChangeDetectorRef) {}

  ngDoCheck(): void {
    console.log('CD on Root', this.cdr);
  }

  onClick() {
    this.data.now = Date.now();

    // Create a new reference if we want to trigger change detection
    // on component using it as an Input and has strategy is OnPush.
    // this.data = {
    //   now: Date.now(),
    // };
  }

}
