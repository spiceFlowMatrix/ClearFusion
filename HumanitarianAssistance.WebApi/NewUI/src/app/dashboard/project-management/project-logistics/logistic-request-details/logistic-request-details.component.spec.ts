import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LogisticRequestDetailsComponent } from './logistic-request-details.component';

describe('LogisticRequestDetailsComponent', () => {
  let component: LogisticRequestDetailsComponent;
  let fixture: ComponentFixture<LogisticRequestDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LogisticRequestDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LogisticRequestDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
