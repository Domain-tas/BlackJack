package com.example.sportoAiksteliuRezervacija.fxControllers;

import com.example.sportoAiksteliuRezervacija.StartGui;
import com.example.sportoAiksteliuRezervacija.ds.User;
import com.example.sportoAiksteliuRezervacija.hibernateControllers.UserHibControl;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.stage.Stage;

import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

public class YourProfileWindow implements Initializable {

    @FXML
    public TextField nameField;
    @FXML
    public TextArea information;
    @FXML
    public TextField email;
    @FXML
    public ImageView imageView;
    User user;
    private int userId;

    Image image;

    EntityManagerFactory entityManagerFactory = Persistence.createEntityManagerFactory("CourseSystemMng");
    UserHibControl userHibControl = new UserHibControl(entityManagerFactory);


    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        imageView = new ImageView();
    }

    public void setYourProfileWindow(int userId) {
        this.userId = userId;
        User user = userHibControl.getUserById(userId);
        //imageView = new ImageView();
        setData(userId, user);
    }

    public void setData(int userId, User user) {
        this.userId = userId;
        this.user = user;
        user = userHibControl.getUserById(user.getId());
        nameField.setText(user.getUsername());
        information.setText(user.getInformation());
        email.setText(user.getEmail());
        nameField.setEditable(false);
        email.setEditable(false);
        information.setEditable(false);
        Image image = new Image(userHibControl.getUserById(userId).getPictureUrl());
        imageView.setImage(image);
    }

    public void editProfile(ActionEvent actionEvent) throws IOException {
        FXMLLoader fxmlLoader = new FXMLLoader(StartGui.class.getResource("edit-profile.fxml"));
        Parent root = fxmlLoader.load();

        EditProfile editProfile = fxmlLoader.getController();
        editProfile.setEditProfile(userId);

        Scene scene = new Scene(root);
        Stage stage = (Stage) nameField.getScene().getWindow();
        stage.setScene(scene);
        stage.show();
    }

    public void back(ActionEvent actionEvent) throws IOException {
        FXMLLoader fxmlLoader = new FXMLLoader(StartGui.class.getResource("main-window.fxml"));
        Parent root = fxmlLoader.load();

        MainWindow mainWindow = fxmlLoader.getController();
        mainWindow.setFormData(userId);

        Scene scene = new Scene(root);
        Stage stage = (Stage) nameField.getScene().getWindow();
        stage.setTitle("Aikštelių rezervacijos sistema");
        stage.setScene(scene);
        stage.show();
    }
}
