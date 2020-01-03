import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAnalyticalInfoComponent } from './add-analytical-info.component';

describe('AddAnalyticalInfoComponent', () => {
  let component: AddAnalyticalInfoComponent;
  let fixture: ComponentFixture<AddAnalyticalInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddAnalyticalInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAnalyticalInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
