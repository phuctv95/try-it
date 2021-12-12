import { Component, OnInit, VERSION } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = `Hello Angular ${VERSION.major}!`;

  form = this.fb.group({
    quantity: [60, [Validators.required, Validators.max(100)]],
    address: [{
      addressLine1: null,
      addressLine2: null,
      zipCode: null,
      city: null,
    }],
  });

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.form.valueChanges.subscribe(console.log);
  }
}
