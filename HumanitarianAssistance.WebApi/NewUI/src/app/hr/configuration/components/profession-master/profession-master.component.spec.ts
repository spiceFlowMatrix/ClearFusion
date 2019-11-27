import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfessionMasterComponent } from './profession-master.component';

describe('ProfessionMasterComponent', () => {
  let component: ProfessionMasterComponent;
  let fixture: ComponentFixture<ProfessionMasterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfessionMasterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfessionMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
