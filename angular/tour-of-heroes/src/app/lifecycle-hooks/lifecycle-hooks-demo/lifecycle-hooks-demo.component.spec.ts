import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LifecycleHooksDemoComponent } from './lifecycle-hooks-demo.component';

describe('LifecycleHooksDemoComponent', () => {
  let component: LifecycleHooksDemoComponent;
  let fixture: ComponentFixture<LifecycleHooksDemoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LifecycleHooksDemoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LifecycleHooksDemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
