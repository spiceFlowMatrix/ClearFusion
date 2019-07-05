import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramAreaSectorComponent } from './program-area-sector.component';

describe('ProgramAreaSectorComponent', () => {
  let component: ProgramAreaSectorComponent;
  let fixture: ComponentFixture<ProgramAreaSectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgramAreaSectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramAreaSectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
