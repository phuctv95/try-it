import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormBuilderGroupComponent } from './form-builder-group.component';

describe('FormBuilderGroupComponent', () => {
  let component: FormBuilderGroupComponent;
  let fixture: ComponentFixture<FormBuilderGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormBuilderGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormBuilderGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
