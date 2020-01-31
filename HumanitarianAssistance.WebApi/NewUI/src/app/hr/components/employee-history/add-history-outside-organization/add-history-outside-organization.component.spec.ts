import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddHistoryOutsideOrganizationComponent } from './add-history-outside-organization.component';

describe('AddHistoryOutsideOrganizationComponent', () => {
  let component: AddHistoryOutsideOrganizationComponent;
  let fixture: ComponentFixture<AddHistoryOutsideOrganizationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddHistoryOutsideOrganizationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddHistoryOutsideOrganizationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
