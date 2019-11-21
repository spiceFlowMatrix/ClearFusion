import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignationListingComponent } from './designation-listing.component';

describe('DesignationListingComponent', () => {
  let component: DesignationListingComponent;
  let fixture: ComponentFixture<DesignationListingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignationListingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignationListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
