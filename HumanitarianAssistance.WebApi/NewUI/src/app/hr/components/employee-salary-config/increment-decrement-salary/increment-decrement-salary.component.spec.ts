import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IncrementDecrementSalaryComponent } from './increment-decrement-salary.component';

describe('IncrementDecrementSalaryComponent', () => {
  let component: IncrementDecrementSalaryComponent;
  let fixture: ComponentFixture<IncrementDecrementSalaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IncrementDecrementSalaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IncrementDecrementSalaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
