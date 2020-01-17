import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignLeaveComponent } from './assign-leave.component';

describe('AssignLeaveComponent', () => {
  let component: AssignLeaveComponent;
  let fixture: ComponentFixture<AssignLeaveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssignLeaveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssignLeaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
