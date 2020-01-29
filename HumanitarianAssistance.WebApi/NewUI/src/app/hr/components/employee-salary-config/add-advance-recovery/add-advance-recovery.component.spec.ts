import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAdvanceRecoveryComponent } from './add-advance-recovery.component';

describe('AddAdvanceRecoveryComponent', () => {
  let component: AddAdvanceRecoveryComponent;
  let fixture: ComponentFixture<AddAdvanceRecoveryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddAdvanceRecoveryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAdvanceRecoveryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
