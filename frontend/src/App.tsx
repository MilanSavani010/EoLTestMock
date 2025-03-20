import { useState } from 'react'
import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Sidebar from './components/Code/AppComponents/SideBar';
import MainContent from './components/Code/AppComponents/MainContent';
import NotFound from './components/Code/MenuPages/NotFound';
import Profile from './components/Code/MenuPages/Profile';
import Matrix from './components/Code/MenuPages/Matrix';
import UnderConstruction from './components/Code/UtilComponents/UnderConstruction';

interface MenuItem {
  id: number;
  label: string;
  path: string;
  icon?: string;
}



function App() {
  const [isSidebarOpen, setIsSidebarOpen] = useState<boolean>(true);

  const menuItems: MenuItem[] = [
    { id: 1, label: 'Home', path: '/', icon: '🏠' },
    { id: 2, label: 'Profile', path: '/profile', icon: '👤' },
    { id: 3, label: 'Matrix', path: '/matrix', icon: '📊' },
    { id: 4, label: 'Settings', path: '/settings', icon: '⚙️' },
  ];


  const handleToggleSidebar = (): void => {
    setIsSidebarOpen(!isSidebarOpen);
  };

  return (
    <BrowserRouter>
      <div className='app'>
        <Sidebar
          isOpen={isSidebarOpen}
          onToggle={handleToggleSidebar}
          menuItems={menuItems}
        />

        <MainContent sidebarOpen={isSidebarOpen}>
            <Routes>
            <Route path="/" element={<UnderConstruction message="Home Page Under Construction" />} />
            <Route path="/profile" element={<Profile />} />
            <Route path="/settings" element={<UnderConstruction message="Setting Page Under Construction" />} />
            <Route path="/matrix" element={<Matrix />} />

            <Route path="*" element={<NotFound />} />        
            </Routes>
        </MainContent>
      </div>
    </BrowserRouter>
  );
};

export default App;
