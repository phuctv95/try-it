import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyTemplateDrivenFormComponent } from './my-template-driven-form.component';

describe('MyTemplateDrivenFormComponent', () => {
  let component: MyTemplateDrivenFormComponent;
  let fixture: ComponentFixture<MyTemplateDrivenFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyTemplateDrivenFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyTemplateDrivenFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
