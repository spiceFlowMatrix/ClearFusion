import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HiringRequestsComponent } from './hiring-requests.component';

describe('HiringRequestsComponent', () => {
  let component: HiringRequestsComponent;
  let fixture: ComponentFixture<HiringRequestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HiringRequestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HiringRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
