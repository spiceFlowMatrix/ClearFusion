import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitComparativeStatementComponent } from './submit-comparative-statement.component';

describe('SubmitComparativeStatementComponent', () => {
  let component: SubmitComparativeStatementComponent;
  let fixture: ComponentFixture<SubmitComparativeStatementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubmitComparativeStatementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmitComparativeStatementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
