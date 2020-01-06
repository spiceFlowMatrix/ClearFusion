import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOfficeMasterComponent } from './add-office-master.component';

describe('AddOfficeMasterComponent', () => {
  let component: AddOfficeMasterComponent;
  let fixture: ComponentFixture<AddOfficeMasterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddOfficeMasterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddOfficeMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
