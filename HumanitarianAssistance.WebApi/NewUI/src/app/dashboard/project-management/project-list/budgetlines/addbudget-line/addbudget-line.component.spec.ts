import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddbudgetLineComponent } from './addbudget-line.component';

describe('AddbudgetLineComponent', () => {
  let component: AddbudgetLineComponent;
  let fixture: ComponentFixture<AddbudgetLineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddbudgetLineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddbudgetLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
