import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorMasterComponent } from './donor-master.component';

describe('DonorMasterComponent', () => {
  let component: DonorMasterComponent;
  let fixture: ComponentFixture<DonorMasterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DonorMasterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DonorMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
