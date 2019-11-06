import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LogisticRequestsComponent } from './logistic-requests.component';

describe('LogisticRequestsComponent', () => {
  let component: LogisticRequestsComponent;
  let fixture: ComponentFixture<LogisticRequestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LogisticRequestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LogisticRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
