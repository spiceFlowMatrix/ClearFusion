import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSalaryConfigurationComponent } from './add-salary-configuration.component';

describe('AddSalaryConfigurationComponent', () => {
  let component: AddSalaryConfigurationComponent;
  let fixture: ComponentFixture<AddSalaryConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddSalaryConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddSalaryConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
