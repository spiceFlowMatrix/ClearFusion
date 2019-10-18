import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorMasterListComponent } from './donor-master-list.component';

describe('DonorMasterListComponent', () => {
  let component: DonorMasterListComponent;
  let fixture: ComponentFixture<DonorMasterListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DonorMasterListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DonorMasterListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
