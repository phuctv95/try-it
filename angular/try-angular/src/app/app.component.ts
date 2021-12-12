import { Component, VERSION } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = `Hello Angular ${VERSION.major}!`;

  form = this.fb.group({
    quantity: [60, [Validators.required, Validators.max(100)]],
  });

  constructor(private fb: FormBuilder) {}
}
