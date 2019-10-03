import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProcurementsComponent } from './add-procurements.component';

describe('AddProcurementsComponent', () => {
  let component: AddProcurementsComponent;
  let fixture: ComponentFixture<AddProcurementsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddProcurementsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddProcurementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
