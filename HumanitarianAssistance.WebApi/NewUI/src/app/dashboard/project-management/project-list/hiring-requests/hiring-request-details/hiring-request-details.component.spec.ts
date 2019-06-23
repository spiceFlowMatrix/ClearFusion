import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HiringRequestDetailsComponent } from './hiring-request-details.component';

describe('HiringRequestDetailsComponent', () => {
  let component: HiringRequestDetailsComponent;
  let fixture: ComponentFixture<HiringRequestDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HiringRequestDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HiringRequestDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
