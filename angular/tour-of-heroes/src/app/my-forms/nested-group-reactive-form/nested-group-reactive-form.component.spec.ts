import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NestedGroupReactiveFormComponent } from './nested-group-reactive-form.component';

describe('NestedGroupReactiveFormComponent', () => {
  let component: NestedGroupReactiveFormComponent;
  let fixture: ComponentFixture<NestedGroupReactiveFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NestedGroupReactiveFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NestedGroupReactiveFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
