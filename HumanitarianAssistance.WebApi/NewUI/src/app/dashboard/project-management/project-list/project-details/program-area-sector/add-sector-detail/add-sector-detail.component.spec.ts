import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSectorDetailComponent } from './add-sector-detail.component';

describe('AddSectorDetailComponent', () => {
  let component: AddSectorDetailComponent;
  let fixture: ComponentFixture<AddSectorDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddSectorDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddSectorDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
