import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddHistoryOutsideCountryComponent } from './add-history-outside-country.component';

describe('AddHistoryOutsideCountryComponent', () => {
  let component: AddHistoryOutsideCountryComponent;
  let fixture: ComponentFixture<AddHistoryOutsideCountryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddHistoryOutsideCountryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddHistoryOutsideCountryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
