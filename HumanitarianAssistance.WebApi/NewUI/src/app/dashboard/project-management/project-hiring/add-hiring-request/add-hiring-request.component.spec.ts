import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddHiringRequestComponent } from './add-hiring-request.component';

describe('AddHiringRequestComponent', () => {
  let component: AddHiringRequestComponent;
  let fixture: ComponentFixture<AddHiringRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddHiringRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddHiringRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
