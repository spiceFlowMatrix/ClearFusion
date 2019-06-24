import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCandidateDaialogComponent } from './add-candidate-daialog.component';

describe('AddCandidateDaialogComponent', () => {
  let component: AddCandidateDaialogComponent;
  let fixture: ComponentFixture<AddCandidateDaialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddCandidateDaialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCandidateDaialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
