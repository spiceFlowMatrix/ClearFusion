import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddHiringRequestsComponent } from './add-hiring-requests.component';

describe('AddHiringRequestsComponent', () => {
  let component: AddHiringRequestsComponent;
  let fixture: ComponentFixture<AddHiringRequestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddHiringRequestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddHiringRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
